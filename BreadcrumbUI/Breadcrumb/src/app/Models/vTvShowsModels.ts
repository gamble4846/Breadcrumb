export interface vTvShowsModel {
    breadId: string | null;
    showId: string | null;
    primaryName: string;
    otherNames: string;
    description: string;
    type: string;
    isStared: boolean;
    iMDBID: string;
    releaseYear: string;
    genres: string;
    genreGUIDs: string;
}

export interface vTvShowsViewModel {
    primaryName: string;
    otherNames: string;
    description: string;
    iMDBID: string;
    releaseYear: string;
    genres: string;
}