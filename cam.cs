using System;
using System.IO;
using System.Web.UI.WebControls;
using Xamarin.Essentials;

namespace YourNamespace
{
    public partial class YourPage : System.Web.UI.Page
    {
protected void Page_Load(object sender, EventArgs e)
{
    if (!IsPostBack)
    {
        // קריאה לפונקציה הרלוונטית כאשר הדף נטען לראשונה
        CallRelevantFunctionOnFirstLoad();
    }
}

private void CallRelevantFunctionOnFirstLoad()
{
            InitializePage(); // קריאה לפונקציה שמאתחלת את הדף
            LoadInitialData(); // קריאה לפונקציה שמביאה נתונים ראשוניים לדף
            SetDefaultValues(); // קריאה לפונקציה שמגדירה ערכים ברירת מחדל לשדות בטופס}

        protected async void OpenCameraButton_Click(object sender, EventArgs e)
        {
            // בקשת אישור לפתיחת המצלמה
            var status = await Permissions.RequestAsync<Permissions.Camera>();

            if (status == PermissionStatus.Granted)
            {
                // פתיחת המצלמה
                var photo = await MediaPicker.CapturePhotoAsync();

                // שמירת התמונה בגלריה
                var filePath = Path.Combine(FileSystem.AppDataDirectory, "photo.jpg");
                await File.WriteAllBytesAsync(filePath, await photo.OpenReadAsync());

                // כאן אתה    מצגת התמונה בטופס 
                        Image1.ImageUrl = "~/AppData/photo.jpg";
 
            }
            else if (status == PermissionStatus.Denied)
            {
                // המשתמש דחה את הגישה למצלמה
        Response.Write("<script>alert('Permission to access camera was denied.');</script>");
                    }
            else if (status == PermissionStatus.Disabled)
            {
                // הגישה למצלמה מושבתת על ידי המשתמש
        Response.Write("<script>alert('Access to camera is disabled. Please enable it in your device settings.');</script>");
            }
        }

        protected async void OpenGalleryButton_Click(object sender, EventArgs e)
{
    // בקשת אישור לגישה לגלריה
    var status = await Permissions.RequestAsync<Permissions.Photos>();

    if (status == PermissionStatus.Granted)
    {
        // פתיחת הגלריה לבחירת תמונה
        var photo = await MediaPicker.PickPhotoAsync();

        if (photo != null)
        {
            // שמירת התמונה בגלריה
            var filePath = Path.Combine(FileSystem.AppDataDirectory, "photo.jpg");
            await File.WriteAllBytesAsync(filePath, await photo.OpenReadAsync());
                // כאן אתה    מצגת התמונה בטופס 

            // לדוגמה, נציג את התמונה בתמונת ImageButton בשם Image2:
            Image2.ImageUrl = "~/AppData/photo.jpg";
        }
    }
    else if (status == PermissionStatus.Denied)
    {
        // המשתמש דחה את הגישה לגלריה
        Response.Write("<script>alert('Permission to access gallery was denied.');</script>");
    }
    else if (status == PermissionStatus.Disabled)
    {
        // הגישה לגלריה מושבתת על ידי המשתמש
        Response.Write("<script>alert('Access to gallery is disabled. Please enable it in your device settings.');</script>");
    }
}

    }
}
