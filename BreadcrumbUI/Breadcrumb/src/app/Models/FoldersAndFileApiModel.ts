export interface FolderAPI {
    name: string;
    id: string;
    files: FilesApi[];
    folders: FolderAPI[];
}

export interface FilesApi {
    name: string;
    thumbnailLink: string;
    type: string;
    size: string;
    email: string;
    id: string;
}

export interface FinalFile{
    FileChunks: Array<FilesApi>;
    name: string;
    thumbnailLink: string;
    type: string;
}