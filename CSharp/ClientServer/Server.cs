﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebSharper;
using WebSharper.Sitelets;
using WebSharper.UI;
using static WebSharper.UI.Html;

namespace $safeprojectname$
{
    public class Server
    {
        [EndPoint("/")]
        public class Home {
            public override bool Equals(object obj) => obj is Home;
            public override int GetHashCode() => 0;
        }

        [EndPoint("GET /about")]
        public class About {
            public override bool Equals(object obj) => obj is About;
            public override int GetHashCode() => 1;
        }
$if$ ($visualstudioversion$ >= 15.0)
        public static Doc MenuBar(Context<object> ctx, object endpoint)
        {
            Doc link(string txt, object act) =>
                li(
                    endpoint.Equals(act) ? attr.@class("active") : null,
                    a(attr.href(ctx.Link(act)), txt)
                );
            return doc(
                li(link("Home", new Home())),
                li(link("About", new About()))
            );
        }
$else$
        public static Doc Link(Context<object> ctx, object endpoint, string txt, object act) =>
            li(
                endpoint.Equals(act) ? attr.@class("active") : null,
                a(attr.href(ctx.Link(act)), txt)
            );

        public static Doc MenuBar(Context<object> ctx, object endpoint) =>
            doc(
                li(Link(ctx, endpoint, "Home", new Home())),
                li(Link(ctx, endpoint, "About", new About()))
            );
$endif$
        public static Task<Content> Page(Context<object> ctx, object endpoint, string title, Doc body) =>
            Content.Page(
                new Template.Main()
                    .Title(title)
                    .MenuBar(MenuBar(ctx, endpoint))
                    .Body(body)
                    .Doc()
            );

        [Website]
        public static Sitelet<object> Main =>
            new SiteletBuilder()
                .With<Home>((ctx, action) =>
                    Page(ctx, action, "Home",
                        doc(
                            h1("Say Hi to the server!"),
                            div(client(() => Client.Main()))
                        )
                    )
                )
                .With<About>((ctx, action) =>
                    Page(ctx, action, "About",
                        doc(
                            h1("About"),
                            p("This is a template WebSharper client-server application.")
                        )
                    )
                )
                .Install();
    }
}