using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartHome.Web.Views.Shared.TagHelpers
{
    /// <summary>
    /// Helper for temperature card.
    /// </summary>
    [HtmlTargetElement(TagStructure = TagStructure.WithoutEndTag)]
    public class TemperatureCardTagHelper : TagHelper
    {
        public bool Inside { get; set; }
        public string? Title { get; set; }
        
        public double? Temperature { get; set; }
        
        public string? Url { get; set; }
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Url != null)
            {
                output.PreElement.AppendHtml($"<a href='{Url}' class='card-anchor'>");
            }
            
            string insideOrOutsideClass = Inside ? "temp-inside" : "temp-outside";
            output.TagName = "div";
            output.Attributes.Add("class", $"e-card temp-card {insideOrOutsideClass}");
            output.Content.AppendHtml($@"<div class='e-card-header'>
                                            <div class='e-card-header-caption'>{Title}</div>
                                        </div>");
            output.Content.AppendHtml($@"<div class='e-card-header'>
                                            <div class='e-card-header-caption'>{Temperature:F1}°C</div>
                                        </div>");

            if (Url != null)
            {
                output.PostElement.AppendHtml("</a>");
            }
            
            base.Process(context, output);
        }
    }
}