import { tbFileDataChunksModel } from "src/app/Models/tbFileDataChunksModel";

export interface AllFilesModel {
    ALLFileModelID: string;
    showFileId: string | null;
    fileId: string | null;
    description: string | null;
    type: string | null;
    name: string | null;
    thumbnailLink: string | null;
    showId: string | null;
    seasonId: string | null;
    episodeId: string | null;
    quality: string | null;
    audioLanguages: string | null;
    subtitleLanguages: string | null;
    chunks: Array<tbFileDataChunksModel> | null;
}