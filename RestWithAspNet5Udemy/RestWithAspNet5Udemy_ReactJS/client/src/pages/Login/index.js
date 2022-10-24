//Passando propriedades para o HTML
//#region 1a maneira
/*
import React from 'react';
export default function Header({ props }) {
    return (
        <header>
            <h1>{props.title}</h1>
        </header>
    );
}
*/
//#endregion

//#region 2a maneira
/*
import React from 'react';
export default function Header({ children }) {
    return (
        <header>
            <h1>{children}</h1>
        </header>
    );
}
*/
//#endregion

import React, { useState } from 'react';
import { useHistory } from 'react-router-dom';
import api from '../../services/api';
import './styles.css';
import logoImage from '../../assets/logo.svg';
import padlock from '../../assets/padlock.png';

export default function Login() {
    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');
    const history = useHistory('');

    async function Login(e) {
        e.preventDefault();

        const data = {
            userName,
            password,
        };

        try {
            const response = await api.post('api/Auth/v1/signin', data);

            localStorage.setItem('userName', userName);
            localStorage.setItem('accessToken', response.data.accessToken);
            localStorage.setItem('refreshToken', response.data.refreshToken);

            history.push('/books');
        } catch (error) {
            alert('Login failed! Try again!');
        }
    }

    return (
        <div className="login-container">
            <section className="form">
                <img src={logoImage} alt="Erudio Logo" />

                <form onSubmit={Login}>
                    <h1>Access your Account</h1>

                    <input placeholder="Username" value={userName} onChange={e => setUserName(e.target.value)} />
                    <input type="password" placeholder="Password" value={password} onChange={e => setPassword(e.target.value)} />

                    <button className="button" type="submit">Login</button>
                </form>
            </section>

            <img className="padlockImage" src={padlock} alt="Login" />
        </div>
    );
}
