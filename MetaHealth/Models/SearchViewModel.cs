﻿using System;
using System.Text;

namespace MetaHealth.Models {

    public class SearchViewModel {

        //components of search result that we'll need
        public string therapyZipcode { get; set; }

        public string therapyCat { get; set; }
        public string therapySpec { get; set; }
        public string mainURL { get; set; } 
        //we want this url to stay the same throughout
    }
}