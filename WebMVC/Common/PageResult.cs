using System;
using System.Collections.Generic;
using System.Text;

namespace WebMVC.Common
{
    public class PageResult<T> : PageResultBase
    {
        public List<T> Items { get; set; }
    }
}