using System;
using System.Collections.Generic;
using System.Text;
using AndroidBox.API;

namespace AndroidBox.DevGuide
{
    public class ContentPanel : AndroidBox.API.ContentPanel
    {

        private string DB = @"\Android_Dev_Guide\db.dat";

        public override void OnLoadUpdate()
        {
            new UpdateContent(CurrentConfig).Update(new EventHandler(Refresh), service, "开发者指南", "Android_Dev_Guide");
        }

        public override string GetDB()
        {
            return DB;
        }
    }
}
