﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartHome.Shared;
using SmartHome.Shared.Models;

namespace SmartHome.Web.Utils
{
    public static class SelectListItemsExtensions
    {
        /// <summary>
        /// Prepends empty value to <see cref="enumerable"/>.
        /// Allows user to select "no option" in select html element.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> PrependEmptyValue(
            this IEnumerable<SelectListItem> enumerable)
        {
            yield return new SelectListItem()
            {
                Value = string.Empty,
                Text = "-"
            };

            foreach (var item in enumerable)
            {
                yield return item;
            }
        }

        /// <summary>
        /// Converts <see cref="enumerable"/> to select list.
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="valueSelector">Selects value. For rendering, <see cref="object.ToString"/> is called on the result.</param>
        /// <param name="textSelector"></param>
        /// <param name="prependEmptyValue"></param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> ToSelectListItems<TType, TValue>(
            this IEnumerable<TType> enumerable,
            Func<TType, TValue> valueSelector,
            Func<TType, string> textSelector,
            bool prependEmptyValue = true)
        {
            var result = enumerable
                .ToSelectListItemsPrivate(valueSelector, textSelector);

            if (prependEmptyValue)
            {
                result = result.PrependEmptyValue();
            }

            return result;
        }

        /// <summary>
        /// Obtains <see cref="SelectListItem"/>s that can be used in html select elements.
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="valueSelector"></param>
        /// <param name="textSelector"></param>
        /// <returns></returns>
        private static IEnumerable<SelectListItem>
            ToSelectListItemsPrivate<TType, TValue>(
                this IEnumerable<TType> enumerable,
                Func<TType, TValue> valueSelector,
                Func<TType, string> textSelector)
        {
            foreach (var item in enumerable)
            {
                yield return new SelectListItem()
                {
                    Value = valueSelector(item).ToString(),
                    Text = textSelector(item)
                };
            }
        }
    }
}