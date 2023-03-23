using Breadcrumb.Model.FilesModels;
using Breadcrumb.Model.tbCoversModels;
using Breadcrumb.Model.tbEpisodesModels;
using Breadcrumb.Model.tbSeasonsModel;
using Breadcrumb.Model.vTvShowsModels;
using Breadcrumb.Utility;

namespace Breadcrumb.DataAccess.SQLServer.Interface
{
    public interface IFilesDataAccess
    {
        List<tbFilesDataModel> SPInsertMultipleFiles(List<tbFilesDataViewModel> tbFilesDataViewModel);
        List<tbFileDataChunksModel> SPInsertMultipleFilesChunks(List<tbFileDataChunksViewModel> tbFileDataChunksViewList);
        List<vNotAssignedFilesModel> GetNotAssignedFiles();
    }
}

