using System.Web.Mvc;
using MetaHealth.Models;
using System.Text;
using System;

namespace MetaHealth.Controllers {

    public class SearchController : Controller {
        FormCollection formObj = new FormCollection();

        // GET: Search
        public ActionResult Index() {
            //This will be used for the error page
            return View();
        }
       [AllowAnonymous] 
        public ActionResult SearchPage() {
            return View();
        }

        //[AllowAnonymous]
        //public ActionResult FindTherapist() {
        //    return RedirectToAction("Index", "Home");
        //}
        //[AllowAnonymous]
        //public ActionResult FindTherapist(FormCollection formObj) {
        //    SearchViewModel retObj = new SearchViewModel();
        //    retObj.mainURL = "https://www.psychologytoday.com/us/";
        //    retObj.therapyCat = Request.Form["therapyCat"].ToString();
        //    retObj.therapySpec = formObj["therapySpec"].ToString();
        //    retObj.therapyZipcode = formObj["therapyZipcode"].ToString();
        //    //searchItem.therapyCat = formObj.therapyCat;
        //    //searchItem.therapySpec = formObj.therapySpec;
        //    //searchItem.therapyZipcode = formObj.therapyZipcode;
        //    string url = TherapistURL(retObj);//this should be handling it's own null issues
        //    return new RedirectResult(url);
        //}

        [AllowAnonymous]
        public ActionResult FindTherapist([Bind(Include = "therapyCat,therapyZipcode,therapySpec")] SearchViewModel formObj) {
            formObj.mainURL = "https://www.psychologytoday.com/us/";
            string url = TherapistURL(formObj);
            return new RedirectResult(url);
        }

        [AllowAnonymous]
        public string TherapistURL(SearchViewModel searchView) {
            StringBuilder retVal = new StringBuilder(searchView.mainURL);//The return will at least direct users to the website itself if nothing else
            if (!String.IsNullOrEmpty(searchView.therapyCat)) {
                retVal.Append(searchView.therapyCat + "/");
            }
            if (!String.IsNullOrEmpty(searchView.therapySpec)) {
                retVal.Append(searchView.therapySpec + "/");
            }
            if (!String.IsNullOrEmpty(searchView.therapyZipcode)) {
                retVal.Append(searchView.therapyZipcode + "/");
            }
            return retVal.ToString();
        }
    }
}