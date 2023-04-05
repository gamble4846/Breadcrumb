import { FilesWithShowToInsertModel, tbFilesDataModel } from "./tbFilesDataModel";

export interface tbEpisodesModel {
    id: string | null;
    seasonId: string | null;
    number: number;
    name: string;
    relaseDate: Date;
    description: string;
    thumbnailLink: string;
}

export interface tbEpisodesViewModel {
    seasonId: string | null;
    number: number;
    name: string;
    relaseDate: Date;
    description: string;
    thumbnailLink: string;
}

export interface EpsiodesDataWithFilesModel{
    id: string | null;
    number: number;
    name: string;
    files: Array<FilesWithShowToInsertModel>;
}