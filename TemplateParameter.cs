using Penguin.Cms.Entities;

namespace Penguin.Cms.Pages
{
    public class TemplateParameter : AuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        public static implicit operator Penguin.Templating.Abstractions.TemplateParameter(TemplateParameter source)
        {
            if (source is null)
            {
                throw new System.ArgumentNullException(nameof(source));
            }

            return new Templating.Abstractions.TemplateParameter
            {
                Name = source.Name,
                Value = source.Value,
                Type = typeof(string)
            };
        }
    }
}