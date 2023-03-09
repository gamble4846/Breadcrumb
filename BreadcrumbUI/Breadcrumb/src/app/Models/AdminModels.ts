export interface Server {
    databaseType: string;
    connectionString: string;
    isSelected: boolean;
}

export interface TokenModel {
    servers: Server[];
    theMovieDBAPIKey: string;
}