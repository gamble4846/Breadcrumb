export interface tbFilesDataModel {
    id: string;
    description: string;
    type: string;
    name: string;
    thumbnailLink: string;
}

export interface FilesWithShowToInsertModel {
    id: string;
    description: string;
    type: string;
    name: string;
    thumbnailLink: string;
    quality: string;
    audioLanguages:string;
    subtitleLanguages:string;
    tbShowsFileID: string | null;
}