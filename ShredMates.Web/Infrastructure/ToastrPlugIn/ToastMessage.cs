﻿using System;

namespace ShredMates.Web.Infrastructure.ToastrPlugIn
{
    [Serializable]
    public class ToastMessage
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public ToastType ToastType { get; set; }

        public bool IsSticky { get; set; }
    }
}
