using Loxifi;
using Penguin.Cms.Entities;
using Penguin.Extensions.String;
using Penguin.Persistence.Abstractions.Attributes.Control;
using Penguin.Persistence.Abstractions.Attributes.Rendering;
using Penguin.Templating.Abstractions.Interfaces;
using System;
using System.Collections.Generic;

namespace Penguin.Cms.Pages
{
    public class Page : AuditableEntity, IModifiableEntity
    {
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

        public enum PageType
        {
            WYSIWYG,
            HTML,
            CSS,
            JS
        }

        public Page()
        {
            Parameters = new List<TemplateParameter>();
        }

        public static PageType GetPageType(string Url)
        {
            if (Url != null)
            {
                if (Url.Length > 3 && string.Equals(Url.Right(4), ".css", StringComparison.OrdinalIgnoreCase))
                {
                    return PageType.CSS;
                }
                else if (Url.Length > 2 && string.Equals(Url.Right(3), ".js", StringComparison.OrdinalIgnoreCase))
                {
                    return PageType.JS;
                }
            }

            return PageType.HTML;
        }

        public override string ToString()
        {
            return Url ?? string.Empty;
        }
    }
}