export interface vMoviesModel {
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

export interface vMoviesViewModel {
    primaryName: string;
    otherNames: string;
    description: string;
    iMDBID: string;
    releaseYear: string;
    genres: string;
}