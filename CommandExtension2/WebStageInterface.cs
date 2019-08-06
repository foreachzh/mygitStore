using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using CsharpHttpHelper;
using Newtonsoft.Json;


namespace CommandExtension2
{
    public class WebStageInterface
    {
        private static WebStageInterface _instance = null;
        public static WebStageInterface Instance()
        {
            if (_instance == null)
                _instance = new WebStageInterface();
            return _instance;
        }

        private bool bLogin = false;
        private HttpHelper tool = new HttpHelper();
        public bool Login()
        {
            return true;
        }

        private string _cookie = null;
        public bool AddItem2Menu(string menuName, string menuURL, int sortOrder = 100, bool isShow = true)
        {
            string uid = AppSetting.GetApp("uid", "13431142222");
            string pwd = AppSetting.GetApp("pwd", "111111");
            string destURL = AppSetting.GetApp("destURL", "http://localhost:10363");
            string theActionIds = "d0739946-99e0-4770-8d2a-6faf25a8457a,1c613ade-6665-49fb-b298-faf1b82dd6d2,cf8f418e-2468-4f6c-96cf-3302a2d9fa9b,15b73068-6c0f-49c9-800c-a7216cb2d079,630e0452-00d6-4452-8a5c-19b2c3115ee3";
            string actionIds = AppSetting.GetApp("actionIds", theActionIds);
            string sys_mid = AppSetting.GetApp("sys_mid", "33c3179e-1ddc-4655-a65e-0b27ef10da8d");
            string parentmenuid = AppSetting.GetApp("parentmenuid", "80a620b6-175a-493f-b7b5-54c61c1fd1e9");
            //商品中心: d96d00a2-5967-4583-b185-8135981ef10a
            string UpdateURL = AppSetting.GetApp("UpdateEqpURL", destURL + "/SysAdmin/ajax.html?sys_method=Add&sys_objName=Sys_Menu&sys_tableName=Sys_Menu&sys_rnd=0.2536368471534731");
            string LoginURL = AppSetting.GetApp("LoginURL", destURL + "/ajax.html?sys_method=Login&sys_rnd=0.6262289755116734");
            string IndexURL = AppSetting.GetApp("IndexURL", destURL + "/default/index.html");
            string referURL = destURL + "/SysAdmin/MenuEdit.html";
            // "http://iot.xinanxrf.com:90/job/EqpList.html"
            // HttpHelper.WebApiUrl = UpdateURL;
            // 如果没有登录，先登录
            string postdata = string.Empty;
            if (!bLogin)
            {
                postdata = string.Format("uid={0}&pwd={1}&r=38", uid, pwd);
                HttpItem loginItem = new HttpItem() { URL = LoginURL, Method = "Post", Postdata = postdata, Referer = destURL + "/login.html", ContentType = "application/x-www-form-urlencoded" };
                HttpResult result = tool.GetHtml(loginItem);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                { //字符串转对象
                    ResponseState state = JsonConvert.DeserializeObject<ResponseState>(result.Html);
                    if (state.success == true)
                    {
                        _cookie = result.Cookie;
                        loginItem = new HttpItem() { URL = IndexURL, Method = "Get", Referer = destURL + "/login.html", ContentType = "application/x-www-form-urlencoded", Cookie = result.Cookie };
                        result = tool.GetHtml(loginItem);
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            System.Net.CookieCollection c1 = HttpHelper.StrCookieToCookieCollection(_cookie);
                            System.Net.CookieCollection c2 = HttpHelper.StrCookieToCookieCollection(result.Cookie);
                            c1.Add(c2);
                            _cookie = HttpHelper.CookieCollectionToStrCookie(c1);
                            bLogin = true;
                        }
                        // _cookie = result.CookieCollection;
                    }
                }
            }

            if (bLogin)
            {
                postdata = string.Format("sys_mid={0}&menuname={1}&menuurl={2}&sortorder={3}&isShow={4}&menuid=&actionids={5}&parentmenuid={6}&menulevel={7}",
                    sys_mid, menuName, menuURL, sortOrder, isShow ? 1 : 0, actionIds, parentmenuid, 4);

                HttpItem postItem = new HttpItem()
                {
                    URL = UpdateURL,
                    Method = "POST",
                    Cookie = _cookie,
                    Postdata = postdata,
                    Referer = referURL,
                    ContentType = "application/x-www-form-urlencoded"
                };
                HttpResult retValue = tool.GetHtml(postItem);
                if (retValue.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ResponseState state = JsonConvert.DeserializeObject<ResponseState>(retValue.Html);
                    if (state.success)
                    {
                        return true;
                    }
                    else
                        Debug.WriteLine("发送消息至服务器操作设备失败，Reason=" + state.msg + ";postdata=" + postdata);
                    return false;
                }
                else
                    return false;
            }
            else
                return false;
        }
    }

    public class ResponseState
    {
        public bool success { get; set; }
        public string msg { get; set; }
    }
}
