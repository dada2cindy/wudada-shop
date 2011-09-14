using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.page;
using com.wudada.web.util;
using Google.GData.Client;
using Google.GData.Photos;
using System.Collections.ObjectModel;
using Common.Logging;
using System.IO;
using Google.GData.Extensions.MediaRss;
using Google.GData.Extensions;
using Google.Picasa;

public partial class testAlbum : BasePage
{
    string GOOGLE_ACCOUNT = ConfigHelper.GoogleAccount;
    string GOOGLE_PASS = ConfigHelper.GooglePass;
    private Service service = new PicasaService("Picasa");
    private PicasaService picasaservice = new PicasaService("Picasa");
    private string authToken;
    private string user, password;
    private PicasaFeed picasafeed;


    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        Button1_Click(null, null);
        //album(GOOGLE_ACCOUNT, GOOGLE_PASS);
    }

    void album(string username, string userpass)
    {
        this.service.setUserCredentials(username, userpass);
        this.authToken = this.service.QueryAuthenticationToken();
        //Response.Write(this.authToken);
        picasaservice.SetAuthenticationToken(this.service.QueryAuthenticationToken());
        string albumTitle = "";
        string albumUri = "";
        
        AlbumQuery query = new AlbumQuery();
        query.Uri = new Uri(PicasaQuery.CreatePicasaUri(this.user));
        this.picasafeed = this.picasaservice.Query(query);
        foreach (PicasaEntry entry in this.picasafeed.Entries)
        {
            albumTitle = entry.Title.Text;
            albumUri = entry.FeedUri;
            Response.Write(entry.Title.Text);
            Response.Write("<br />");
            
            MediaThumbnail thumb = entry.Media.Thumbnails[0];
            try
            {
                //Stream stream = picasaservice.Query(new Uri(thumb.Attributes["url"] as string));
                Image1.ImageUrl = thumb.Attributes["url"].ToString();
            }
            catch (Exception ex)
            {
            }
        }

        log.Info(albumTitle);
        log.Info(albumUri);
        PhotoQuery photoQuery = new PhotoQuery(albumUri);
        this.picasafeed = this.picasaservice.Query(photoQuery);
        foreach (PicasaEntry entry in this.picasafeed.Entries)
        {
            try
            {
                Photo photo = new Photo();
                photo.AtomEntry = entry;
                //Image2.ImageUrl = findLargestThumbnail(entry.Media.Thumbnails);
                Image2.ImageUrl = photo.PhotoUri.AbsoluteUri;
            }
            catch (Exception ex)
            {
            }
        }
    }

    private string findLargestThumbnail(ExtensionCollection<MediaThumbnail> collection)
    {
        MediaThumbnail largest = null;
        int width = 0;
        foreach (MediaThumbnail thumb in collection)
        {
            int iWidth = int.Parse(thumb.Attributes["width"] as string);
            if (iWidth > width)
            {
                largest = thumb;
            }
        }
        return largest.Attributes["url"] as string;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        this.user = GOOGLE_ACCOUNT;
        this.password = GOOGLE_PASS;
        album(this.user, this.password);
    }
}

