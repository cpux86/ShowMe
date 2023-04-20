using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using Shorthand.ImageSharp.WebP;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class UploadController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> UploadImageFile([FromForm] IFormFileCollection uploadedFile)
        {
            var f = uploadedFile;
            var request =  HttpContext.Request;
            if (!request.HasFormContentType || !MediaTypeHeaderValue.TryParse(request.ContentType, out var mediaTypeHeader) || string.IsNullOrEmpty(mediaTypeHeader.Boundary.Value))
            {
                return new UnsupportedMediaTypeResult();
            }
            //var reader = new MultipartReader(mediaTypeHeader.Boundary.Value, request.Body);
            //var section = await reader.ReadNextSectionAsync();
            



            foreach (var item in uploadedFile)
            {
                //var fileName = Path.GetRandomFileName();
                //var saveToPath = Path.Combine(Path.GetTempPath(), fileName);
                var saveToPath = Path.Combine(@"C:\C#\Zion", item.FileName);
                
                using (var targetStream = System.IO.File.Create(saveToPath))
                {
                    //await section.Body.CopyToAsync(targetStream);
                    await item.CopyToAsync(targetStream);
                    
                }

                using (Image image = Image.Load(saveToPath))
                {
                    // Resize the given image in place and return it for chaining.
                    // 'x' signifies the current image processing context.

                    var size = image.Size();
                    var l = size.Width / 4;
                    var t = size.Height / 4;
                    var r = 3 * (size.Width / 4);
                    var b = 3 * (size.Height / 4);

                    //image.Mutate(x => x.Resize(image.Width / 4, image.Height / 4));
                    //image.Mutate(x => x.Resize(new Size { Width = 150, Height = 150}));
                    //image.Mutate(x => x.Resize(new Size { Width = 1280}));
                    //image.Mutate(x => x.Crop(400, 300));

                    

                    await image.SaveAsync(@"C:\C#\Zion\1.webp", new WebPEncoder { Quality = 80 });
                    //image.SaveAsJpeg(@"C:\C#\Zion\1.jpg");


                    //image.Save(@"C:\C#\Zion\1.jpg");


                }
            }

            

            return Ok();
        }
    }
}
