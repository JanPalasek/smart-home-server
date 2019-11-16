using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartHome.Web.Configurations;
using Syncfusion.EJ2.FileManager.Base;
using Syncfusion.EJ2.FileManager.PhysicalFileProvider;

namespace SmartHome.Web.Controllers.ApiControllers
{
    /// <summary>
    /// Controller that handles accessing the disk files.
    /// </summary>
    [Authorize]
    public class FilesController : Controller
    {
        private readonly FileManagerConfiguration fileManagerConfiguration;
        private readonly PhysicalFileProvider operation;
        
        public FilesController(FileManagerConfiguration fileManagerConfiguration)
        {
            this.fileManagerConfiguration = fileManagerConfiguration;
            
            operation = new PhysicalFileProvider();
            // Assign the mapped path as root folder
            operation.RootFolder(fileManagerConfiguration.StoragePath);
        }

        [Route("api/files/operations")]
        public object FileOperations([FromBody] FileManagerDirectoryContent args)
        {
            // Restricting modification of the root folder
            if ((args.Action == "delete" || args.Action == "rename") && args.TargetPath == null && args.Path == string.Empty)
            {
                FileManagerResponse response = new FileManagerResponse();
                response.Error = new ErrorDetails()
                {
                    Code = "401",
                    Message = "Restricted to modify the root folder."
                };
                return operation.ToCamelCase(response);
            }
            // Processing the File Manager operations
            FileManagerResponse diskResponse;
            switch (args.Action)
            {
                case "read":
                    // Path - Current path; ShowHiddenItems - Boolean value to show/hide hidden items
                    diskResponse = operation.GetFiles(args.Path, args.ShowHiddenItems);
                    break;
                case "delete":
                    // Path - Current path where of the folder to be deleted; Names - Name of the files to be deleted
                    diskResponse = operation.Delete(args.Path, args.Names);
                    break;
                case "copy":
                    //  Path - Path from where the file was copied; TargetPath - Path where the file/folder is to be copied; RenameFiles - Files with same name in the copied location that is confirmed for renaming; TargetData - Data of the copied file
                    diskResponse = operation.Copy(args.Path, args.TargetPath, args.Names, args.RenameFiles, args.TargetData);
                    break;
                case "move":
                    // Path - Path from where the file was cut; TargetPath - Path where the file/folder is to be moved; RenameFiles - Files with same name in the moved location that is confirmed for renaming; TargetData - Data of the moved file
                    diskResponse = operation.Move(args.Path, args.TargetPath, args.Names, args.RenameFiles, args.TargetData);
                    break;
                case "details":
                    // Path - Current path where details of file/folder is requested; Name - Names of the requested folders
                    diskResponse = operation.Details(args.Path, args.Names);
                    break;
                case "create":
                    // Path - Current path where the folder is to be created; Name - Name of the new folder
                    diskResponse = operation.Create(args.Path, args.Name);
                    break;
                case "search":
                    // Path - Current path where the search is performed; SearchString - String typed in the searchbox; CaseSensitive - Boolean value which specifies whether the search must be casesensitive
                    diskResponse = operation.Search(args.Path, args.SearchString, args.ShowHiddenItems, args.CaseSensitive);
                    break;
                case "rename":
                    // Path - Current path of the renamed file; Name - Old file name; NewName - New file name
                    diskResponse = operation.Rename(args.Path, args.Name, args.NewName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(args.Action));
            }
            
            return operation.ToCamelCase(diskResponse);
        }

        [Route("api/files/upload")]
        [DisableRequestSizeLimit]
        [RequestFormLimits(MultipartBodyLengthLimit = long.MaxValue, ValueLengthLimit = int.MaxValue)]
        public IActionResult Upload(string path, IList<IFormFile> uploadFiles, string action)
        {
            // validate size limit
            if (uploadFiles.Any(x => x.Length > fileManagerConfiguration.MaximumUploadSize))
            {
                return BadRequest("Uploaded files are too large.");
            }
            
            // Use below code for performing upload operation
            // Invoking upload operation with the required parameters
            // path - Current path where the file is to uploaded; uploadFiles - Files to be uploaded; action - name of the operation(upload)
            operation.Upload(path, uploadFiles, action, null);
            return Content(string.Empty);
        }
        
        [Route("api/files/download")]
        public IActionResult Download(string downloadInput)
        {
            FileManagerDirectoryContent args = JsonConvert.DeserializeObject<FileManagerDirectoryContent>(downloadInput);
            // Invoking download operation with the required parameters
            // path - Current path where the file is downloaded; Names - Files to be downloaded;
            return operation.Download(args.Path, args.Names);
        }
        [Route("api/files/getImage")]
        public IActionResult GetImage(FileManagerDirectoryContent args)
        {
            // Invoking GetImage operation with the required parameters
            // path - Current path of the image file; Id - Image file id;
            return operation.GetImage(args.Path, args.Id, false, null, null);
        }
    }
}