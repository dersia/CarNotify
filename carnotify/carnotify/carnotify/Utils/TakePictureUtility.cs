using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Microsoft.ProjectOxford.Vision.Contract;
using Microsoft.ProjectOxford.Vision;
using System;
using carnotify.Constants;

namespace carnotify.Utils
{
    public static class TakePictureUtility
    {
        public static async Task<MediaFile> TakePictureAsync()
        {
            try
            {
                await CrossMedia.Current.Initialize();
                if (CrossMedia.Current.IsCameraAvailable)
                {
                    var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        SaveToAlbum = false,
                        Directory = "Plates",
                        Name = "CurrentPlate.jpg",
                        CompressionQuality = 65
                    });
                    return photo;
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return null;
        }

        public static async Task<string> OcrPictureAsync(MediaFile mediaFile)
        {
            if (mediaFile != null)
            {
                try
                {
                    OcrResults result;

                    var visionApiClient = new VisionServiceClient(ConfigurationConstants.VisionApiApiKey);
                    using (var photoStream = mediaFile.GetStream())
                    {
                        result = await visionApiClient.RecognizeTextAsync(photoStream);
                    }
                    if (result != null)
                    {
                        var text = string.Empty;
                        foreach (var region in result.Regions)
                        {
                            foreach (var line in region.Lines)
                            {
                                foreach (var word in line.Words)
                                {
                                    text += word.Text;
                                }
                            }
                        }
                        return text;
                    }
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            return null;
        }

        public static async Task<string> TakePictureTextAsync()
        {
            var photo = await TakePictureAsync();
            return await OcrPictureAsync(photo);
        }
    }
}
