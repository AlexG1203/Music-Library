export interface IArtist{
    artistId: number;
    artistName: string;
}

export interface IAlbum{
    albumId: number;
    artistId: number;
    albumTitle: string;
    albumDescription: string;
}

export interface ISong{
    songId: number;
    albumId: number;
    songTitle: string;
    songLength: string;
}

export interface ISearchResult {
    artists: any[];
    albums: any[];
    songs: any[];
}
