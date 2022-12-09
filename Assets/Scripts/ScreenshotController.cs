using System.IO;
using UnityEngine;

namespace PC
{
    [RequireComponent(typeof(Camera))]
    public class ScreenshotController : MonoBehaviour
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields
        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields

        [SerializeField] private float _width = 1920;
        [SerializeField] private float _height = 1080;
        private Camera _camera;

        #endregion Private Fields

        #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

        #region Methods
    
        #region Public Methods
        #endregion Public Methods

        #region Protected Methods
        #endregion Protected Methods

        #region Private Methods

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                string dir = Application.dataPath;
                #if UNITY_EDITOR
                    dir = Path.GetFullPath(Path.Combine(dir, @"..\"));
                #endif
                dir = Path.Combine(dir, "Screenshots");

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                
                var path = Path.Combine(dir, GetScreenshotFilename());
                path = PreventFileOverwriting(path);
                ScreenCapture.CaptureScreenshot(path);
                Debug.Log($"Screenshot saved to {path}");
            }
        }

        private string GetTimeCode()
        {
            var datetime = System.DateTime.Now;
            var time = datetime.ToString("HH-mm-ss");
            return time;
        }

        private string GetDateCode()
        {
            var datetime = System.DateTime.Now;
            var date = datetime.ToString("MM-dd-yyyy");
            return date;
        }

        private string GetScreenshotFilename()
        {
            return $"Screenshot_{GetDateCode()}_{GetTimeCode()}.png";
        }

        private string PreventFileOverwriting(string path)
        {
            var filepath = Path.GetDirectoryName(path);
            var filename = Path.GetFileNameWithoutExtension(path);
            var ext = Path.GetExtension(path);
            string result = Path.Combine(filepath, $"{filename}{ext}");
            for(int counter = 1; File.Exists(result); result = Path.Combine(filepath, $"{filename}_{counter++}{ext}"));
            return result;
        }
    
        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}