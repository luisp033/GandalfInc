using System;

namespace Projeto.WebApp.Util
{
    public static class Util
    {

        public static string GetUrlImageFormByteArray(byte[] byteArray)
        {
            string imageDataURL = String.Empty;
            if (byteArray != null)
            {
                string imageBase64Data = Convert.ToBase64String(byteArray);
                imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
            }
            return imageDataURL;
        }
    }
}
