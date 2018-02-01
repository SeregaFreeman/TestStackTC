﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using TestStack.White.UIItems.Finders;
using TestStackFramework.framework.elements;

namespace Views
{
    public class ReplaceOrSkipFilesView
    {
        public static Button ButtonSkip => Button.Get(SearchCriteria.ByControlType(ControlType.Button).AndByText("Skip this file"), "Skip");
    }
}