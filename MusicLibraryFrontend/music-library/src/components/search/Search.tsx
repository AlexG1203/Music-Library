import React, { useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';

import './search.scss';
import '../artists/artists.scss'
import '../albums/albums.scss'
import '../songs/songs.scss'
import {ISearchResult} from "../../types/global.typing";
import artistIcon from "../../assets/images/artistIcon.jpg";
import albumIcon from "../../assets/images/albumIcon.jpg";
import { ReactComponent as NoteIcon } from "../../assets/svgs/note.svg";


const Search: React.FC = () => {
    const [query, setQuery] = useState('');
    const [results, setResults] = useState<ISearchResult>({ artists: [], albums: [], songs: [] });

    const handleSearch = async (e: React.ChangeEvent<HTMLInputElement>) => {
        const q = e.target.value;
        setQuery(q);

        if (q.length >= 1) {
            try {
                const response = await axios.get<ISearchResult>(`https://localhost:7130/api/Search?q=${q}`);
                setResults(response.data);
            } catch (error) {
                console.error('Error fetching search results:', error);
                setResults({ artists: [], albums: [], songs: [] });
            }
        } else {
            setResults({ artists: [], albums: [], songs: [] });
        }
    };

    return (
        <div className="search">
            <input
                type="text"
                value={query}
                onChange={handleSearch}
                placeholder="Search for artists, albums, songs..."
            />
            {results.artists.length > 0 || results.albums.length > 0 || results.songs.length > 0 ? (
                <div className="searchResults">
                    {results.artists.length > 0 && (
                        <div className="cardsWrap">
                            <h1>Artists</h1>
                            <div className="cardsWrapInner">
                                {results.artists.map((artist: any) => (
                                    <div className="card" key={artist.artistId}>
                                        <Link to={'/albums/' + artist.artistId} style={{ textDecoration: "none" }} >
                                            <div className="cardImage">
                                                <img src={artistIcon} alt="Artist" />
                                            </div>
                                            <div className="cardContent">
                                                <h3>{artist.artistName}</h3>
                                            </div>
                                        </Link>
                                    </div>
                                ))}
                            </div>
                        </div>
                    )}


                    {results.albums.length > 0 && (
                        <div className="cardsWrap">
                            <h1>Albums</h1>
                            <div className="cardsWrapInner">
                                {results.albums.map((album: any) => (
                                    <div className="card" key={album.albumId}>
                                        <Link to={'/songs/' + album.albumId} style={{ textDecoration: "none" }}>
                                            <div className="cardImage">
                                                <img src={albumIcon} alt="Album" />
                                            </div>
                                            <div className="cardContent">
                                                <h3>{album.albumTitle}</h3>
                                            </div>
                                        </Link>
                                    </div>
                                ))}
                            </div>
                        </div>
                    )}

                    {results.songs.length > 0 && (
                        <div className="albumPageSongs">
                            <h1>Songs</h1>
                            {results.songs.map((song: any) => (
                                <ul className="songList" >
                                    <li>
                                        <div className="songIcon">
                                            <NoteIcon/>
                                        </div>
                                        <div className="songDetails">
                                            <h3>{song.songTitle}</h3>
                                        </div>
                                        <div className="songTime">
                                            <span>{song.songLength}</span>
                                        </div>
                                    </li>
                                </ul>
                            ))}
                        </div>
                    )}

                </div>
            ) : null}
        </div>
    );
};

export default Search;