import HomePage from './pages/HomePage';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import PlayerScreen from './pages/PlayerScreen';
import ApiProvider from './context/ApiProvider';
import { useState } from 'react';
import { Character } from './model/Model';
import { characterGetAny, characterStore } from './model/ChracterList';

function App() {
	const [characterActive, setCharacterActive] = useState<Character>(characterGetAny());

	const updateCharacter = (c: Character) => {
		setCharacterActive(c);
		characterStore(c);
	}

	return (
		<ApiProvider>
			<Router>
				<Routes>
					<Route path="/" element={<HomePage />} />
					<Route path="/login" element={<LoginPage />} />
					<Route path="/register" element={<RegisterPage />} />
					<Route path="/player-screen" element={<PlayerScreen character={characterActive} setCharacter={updateCharacter} />} />
				</Routes>
			</Router>
		</ApiProvider>
	);
}

export default App;
