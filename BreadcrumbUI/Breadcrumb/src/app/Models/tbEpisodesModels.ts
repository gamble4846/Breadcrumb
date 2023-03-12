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