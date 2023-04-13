import { tbFileDataChunksModel } from "./tbFileDataChunksModel";

export interface FullShowFilesWithChunksModel {
    showFileId: string;
    fileId: string;
    description: string;
    type: string;
    name: string;
    thumbnailLink: string;
    showId: string | null;
    seasonId: string | null;
    episodeId: string | null;
    quality: string;
    audioLanguages: string;
    subtitleLanguages: string;
    chunks: tbFileDataChunksModel[];
}