import React, {useEffect, useState} from "react";
import albumIcon from "../../assets/images/albumIcon.jpg";
import { ReactComponent as NoteIcon } from "../../assets/svgs/note.svg";

import "./songs.scss"
import {IAlbum, ISong} from "../../types/global.typing";
import {useParams} from "react-router-dom";
import axios from "axios";

const Songs: React.FC = () => {
    const [songs, setSongs] = useState<ISong[]>([]);
    const { albumId } = useParams<{ albumId: string }>();
    const fetchSongsList = async (albumId: string) => {
        try {
            const response = await axios.get<ISong[]>(`https://localhost:7130/api/Songs/album/${albumId}`);
            setSongs(response.data);
        } catch (error){
            alert("Server Error")
        }
    }

    useEffect(() => {
        if (albumId) {
            fetchSongsList(albumId);
        }
    }, [albumId]);

    const [album, setAlbum] = useState<IAlbum | null>(null);
    const fetchAlbumsList = async (albumId: string) => {
        try {
            const response = await axios.get<IAlbum>(`https://localhost:7130/api/Albums/${albumId}`);
            setAlbum(response.data);
        } catch (error){
            alert("Server Error")
        }
    }

    useEffect(() => {
        if (albumId) {
            fetchAlbumsList(albumId);
        }
    }, [albumId]);

    return(
        <div className="albumPage">
            <div className="albumPageInfo">
                <div className="albumPageImage">
                    <img src={albumIcon} alt="Album"/>
                </div>
                {album && (
                    <div className="albumPageContent" key={album.albumId}>
                        <h1>{album.albumTitle}</h1>
                        <p className="description">
                            {album.albumDescription}
                        </p>
                    </div>
                )}
            </div>
            <div className="albumPageSongs">
                {songs.map((song) => (
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
        </div>
    )
}

export default Songs