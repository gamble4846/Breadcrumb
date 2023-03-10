export interface tbEpisodesModel {
    id: string | null;
    seasonId: string | null;
    number: number;
    name: string;
    relaseDate: Date;
}

export interface tbEpisodesViewModel {
    seasonId: string | null;
    number: number;
    name: string;
    relaseDate: Date;
}