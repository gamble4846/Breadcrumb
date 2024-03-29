export interface Server {
    databaseType: string;
    connectionString: string;
    isSelected: boolean;
}

export interface TokenModel {
    servers: Server[];
    theMovieDBAPIKey: string;
    showNSFWCovers: boolean;
    showOnlyCustomCovers: boolean;
    googleAPIs: Array<GoogleAPI>;
}

export interface GoogleAPI{
    apiKey: string;
    isPrimary: boolean;
}