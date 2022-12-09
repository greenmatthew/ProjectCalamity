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

        /// <summary>
        /// Get the current time in filename safe format.
        /// </summary>
        /// <returns>Current time in filename safe format.</returns>
        private string GetTimeCode()
        {
            var datetime = System.DateTime.Now;
            var time = datetime.ToString("HH-mm-ss");
            return time;
        }

        /// <summary>
        /// Get the current date in filename safe format.
        /// </summary>
        /// <returns>Current date in filename safe format.</returns>
        private string GetDateCode()
        {
            var datetime = System.DateTime.Now;
            var date = datetime.ToString("MM-dd-yyyy");
            return date;
        }

        /// <summary>
        /// Get the filename for the screenshot.
        /// </summary>
        /// <returns>Filename for the screenshot.</returns>
        private string GetScreenshotFilename()
        {
            return $"Screenshot_{GetDateCode()}_{GetTimeCode()}.png";
        }

        /// <summary>
        /// Prevents overwriting of files by adding or modifying the ending of the filename with a "_N" before the file extension, where N is any integer from 1 to INT.MAX.
        /// Example: "Screenshot.png" -> "Screenshot_1.png"
        /// Example: "Screenshot_1.png" -> "Screenshot_2.png"
        /// Example: "Screenshot_6.png" -> "Screenshot_7.png"
        /// </summary>
        /// <param name="path">Path to the file.</param>
        /// <returns>Path to the file that does not exist.</returns>
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