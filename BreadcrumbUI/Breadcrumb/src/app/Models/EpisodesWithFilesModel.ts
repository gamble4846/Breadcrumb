import { vFullShowFilesModel } from "./vFullShowFilesModel";


export interface EpisodesWithFilesModel {
    id: string | null;
    seasonId: string | null;
    number: number;
    name: string;
    relaseDate: string;
    description: string;
    thumbnailLink: string;
    files: vFullShowFilesModel[];
}