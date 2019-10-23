﻿using Penguin.Cms.Entities;
using Penguin.Extensions.Strings;
using Penguin.Persistence.Abstractions.Attributes.Control;
using Penguin.Persistence.Abstractions.Attributes.Rendering;
using Penguin.Templating.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Penguin.Cms.Pages
{
    [SuppressMessage("Design", "CA1056:Uri properties should not be strings")]
    [SuppressMessage("Usage", "CA2227:Collection properties should be read only")]
    public class Page : AuditableEntity, IModifiableEntity
    {
        public enum PageType
        {
            WYSIWYG,
            HTML,
            CSS,
            JS
        }

        public bool Cascade { get; set; }

        [HtmlRender(HtmlRenderAttribute.RenderingType.html)]
        public string Content { get; set; } = string.Empty;

        public string Layout { get; set; }

        [EagerLoad]
        public List<TemplateParameter> Parameters { get; set; }

        public DateTime? PublishedDate { get; set; }

        //This is dumb. Make it a real field
        public PageType Type { get; set; }

        public string Url { get; set; }

        public Page()
        {
            this.Parameters = new List<TemplateParameter>();
        }

        [SuppressMessage("Design", "CA1054:Uri parameters should not be strings")]
        public static PageType GetPageType(string Url)
        {
            if (Url != null)
            {
                if (Url.Length > 3 && string.Equals(Url.Right(4), ".css", StringComparison.InvariantCultureIgnoreCase))
                {
                    return PageType.CSS;
                }
                else if (Url.Length > 2 && string.Equals(Url.Right(3), ".js", StringComparison.InvariantCultureIgnoreCase))
                {
                    return PageType.JS;
                }
            }

            return PageType.HTML;
        }

        public override string ToString() => this.Url ?? string.Empty;
    }
}