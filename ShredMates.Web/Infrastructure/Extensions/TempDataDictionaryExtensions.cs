﻿using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ShredMates.Web.Infrastructure.Extensions
{
    public static class TempDataDictionaryExtensions
    {
        public static void AddSuccessMessage(this ITempDataDictionary tempData, string message)
        {
            tempData[WebConstants.SuccessMessage] = message;
        }

        public static void AddErrorMessage(this ITempDataDictionary tempData, string message)
        {
            tempData[WebConstants.ErrorMessage] = message;
        }

        public static void AddWarningMessage(this ITempDataDictionary tempData, string message)
        {
            tempData[WebConstants.WarningMessage] = message;
        }
    }
}
