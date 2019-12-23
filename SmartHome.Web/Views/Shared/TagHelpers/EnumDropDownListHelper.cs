using System;
using System.Linq;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartHome.Common.Helpers;
using SmartHome.DomainCore.Data;
using Syncfusion.EJ2.DropDowns;

namespace SmartHome.Web.Views.Shared.TagHelpers
{
    [HtmlTargetElement("enum-dropdownlist")]
    public class EnumDropDownListHelper : DropDownList
    {
        private Type? enumType;

        public Type? EnumType
        {
            get => enumType;
            set
            {
                if (!value.IsEnum)
                {
                    throw new ArgumentException($"{value?.Name} is not of type Enum.");
                }

                enumType = value;
            }
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;
            
            if (EnumType == null)
            {
                throw new ArgumentException("Enum dropdownlist helper doesn't have type set.");
            }
            
            var enumResources = EnumHelper.GetAllValues(EnumType)
                .Select(x =>  new { Value = (int) x, Text = x.ToString() })
                .ToArray();
            
            Fields = new DropDownListFieldSettings()
            {
                Text = "Text",
                Value = "Value"
            };
            DataSource = enumResources;
            
            base.Process(context, output);
        }
    }
}