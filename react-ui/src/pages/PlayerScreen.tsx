import { memo, useContext, useState } from "react";
import { Character, CharacterScreen, ScreenWidget, characterClone } from "../model/Model";
import Drawer from "@mui/material/Drawer"
import { styled, useTheme } from "@mui/material/styles";
import IconButton from "@mui/material/IconButton";
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import ChevronRightIcon from '@mui/icons-material/ChevronRight';
import MenuIcon from '@mui/icons-material/Menu';
import { List, ListItem, Typography, Button, Box, Divider, TextField } from "@mui/material";
import Draggable, { DraggableData, DraggableEvent } from "react-draggable";
import { htmlByWxName } from "../components/widgets/Widgets";
import { useNavigate } from "react-router-dom";
import { Close, Delete } from "@mui/icons-material";
import { ApiContext } from "../context/ApiProvider";
import { useLogout } from "../api/ApiLogon";
import { ApiCharacter, useApiCreateCharacter, useApiGetCharacter, useUpdateCharacter as useApiUpdateCharacter } from "../api/ApiCharacter";
import { characterComposeFromApi, characterModelToApi } from "../api/CharacterConvert";
import { Guid } from "guid-typescript";
import { useApiGetCharacterScreens } from "../api/ApiView";

interface PlayerScreenProps {
	character: Character;
	setCharacter: (character: Character) => void;
}

interface PlayerScreenContentProps {
	character: Character;
	setCharacter: (character: Character) => void;
	activeScreen: number;
	editMode: boolean;
	setLeftOpen: (v: boolean) => void;
	removeWidget: (idx: number) => void;
}

const PlayerScreenContent = memo(function PlayerScreenContent({ character, setCharacter, activeScreen, editMode, setLeftOpen, removeWidget }: PlayerScreenContentProps) {
	const theme = useTheme();
	return (
		<Box>
			<IconButton sx={{ m: 2 }} onClick={() => { setLeftOpen(true); }}>
				<MenuIcon />
			</IconButton>
			{character && (character.screens[activeScreen].widgets.map((wx: ScreenWidget, wxIndex: number) => {
				return (
					<Draggable defaultPosition={{ x: wx.posx, y: wx.posy }} position={{ x: wx.posx, y: wx.posy }} disabled={!editMode}
						onStop={(e: DraggableEvent, data: DraggableData) => {
							let chr = characterClone(character);
							chr.screens[activeScreen].widgets[wxIndex].posx = data.lastX;
							chr.screens[activeScreen].widgets[wxIndex].posy = data.lastY;
							setCharacter(chr);
						}}>
						<Box display="flex" sx={{ position: "absolute", top: "0", left: "0", alignItems: "baseline" }}>
							{htmlByWxName(wx.name)(character, setCharacter, editMode)}
							{editMode && <IconButton color="error" sx={{ width: "25px", height: "25px" }} onClick={() => removeWidget(wxIndex)}><Delete /></IconButton>}
						</Box>
					</Draggable>)
			}))}
		</Box>);
});

function PlayerScreen({ character, setCharacter }: PlayerScreenProps) {
	const navigate = useNavigate();
	const theme = useTheme();
	const authCtx = useContext(ApiContext);
	const [fetchError, apiLogout] = useLogout();
	const [leftOpen, setLeftOpen] = useState(true);
	//const [rightOpen, setRightOpen] = useState(true);
	const [activeScreen, setActiveScreen] = useState(0);
	const [editMode, setEditMode] = useState(false);
	const [characterUpdateResult, updateCharacter] = useApiUpdateCharacter();
	const [characterCreateResult, createCharacter] = useApiCreateCharacter();
	const [characterGetResult, getCharacter] = useApiGetCharacter();
	const [characterViewGetResult, getCharacterView] = useApiGetCharacterScreens();


	const toggleEditMode = () => {
		setEditMode(!editMode);
	}

	const onLogout = async () => {
		await apiLogout();
		localStorage.removeItem("token");
		authCtx.setAuthorised(false);
	}

	const characterToServer = async () => {
		if (character.stored_on_server) {
			await updateCharacter(character.guid, characterModelToApi(character));
		}
		else {
			const onCreateCharacterReceiveGuid = (guid: Guid) => {
				let new_character = characterClone(character);
				new_character.stored_on_server = true;
				new_character.guid = guid;
				setCharacter(new_character);
			}

			await createCharacter(characterModelToApi(character), onCreateCharacterReceiveGuid);
		}
	}

	const characterFromServer = async () => {
		let ch: ApiCharacter = characterModelToApi(character);
		let sc: CharacterScreen[] = character.screens;

		await getCharacter(character.guid, c => ch = c);
		await getCharacterView(character.guid, s => sc = s);

		setCharacter(characterClone(characterComposeFromApi(ch, sc)));
	}

	const exportJson = () => {
		const jsonString = `data:text/json;chatset=utf-8,${encodeURIComponent(
			JSON.stringify(character)
		)}`;
		const link = document.createElement("a");
		link.href = jsonString;
		link.download = character.name + ".json";

		link.click();
	};

	const characterRename = (name: string) => {
		let chr = characterClone(character);
		chr.name = name || "";
		setCharacter(chr);
	};

	const onCharacterNameChange = (ev: React.ChangeEvent<HTMLInputElement>) => {
		ev.preventDefault();
		characterRename(ev.currentTarget.value);
	}

	const removeWidget = (idx: number) => {
		let chr = characterClone(character);
		chr.screens[activeScreen].widgets = chr.screens[activeScreen].widgets.filter((v, i) => i != idx);
		setCharacter(chr);
	}

	const addWidget = (name: string) => {
		let chr = characterClone(character);
		chr.screens[activeScreen].widgets.push({ name: name, posx: 300, posy: 300 });
		setCharacter(chr);
	}

	return (
		<Box sx={{ display: 'flex' }}>
			<PlayerScreenContent character={character} setCharacter={setCharacter} activeScreen={activeScreen} editMode={editMode} setLeftOpen={setLeftOpen} removeWidget={removeWidget} />
			<Drawer variant="persistent" anchor="left" open={leftOpen}>
				<Box display="flex" flexDirection="row" alignItems="center" justifyContent="space-between" sx={{ backgroundColor: theme.palette.secondary.main }}>
					{editMode && (
						<TextField sx={{ m: 2, minWidth: "200px" }} size="medium" variant="standard"
							defaultValue={character.name} onChange={onCharacterNameChange} />
					)}
					{!editMode && (
						<Typography variant="h5" margin={2} minWidth={200}>
							{character.name}
						</Typography>)}
					<IconButton sx={{ m: 2 }} onClick={() => { setLeftOpen(false); }}>
						{theme.direction === 'ltr' ? <ChevronLeftIcon /> : <ChevronRightIcon />}
					</IconButton>
				</Box>
				<List sx={{ "& Button": { variant: "text", p: 1, m: -1, minWidth: 1, justifyContent: "left" } }}>
					{!authCtx.authorised && (<ListItem><Button onClick={() => { navigate("/login"); }}>
						Войти</Button></ListItem>)}
					{!authCtx.authorised && (<ListItem><Button onClick={() => { navigate("/register"); }}>
						Зарегистрироваться</Button></ListItem>)}
					{authCtx.authorised && (<ListItem><Typography>
						{authCtx.getUsername()}</Typography></ListItem>)}
					{authCtx.authorised && (<ListItem><Button onClick={characterToServer}>
						Загрузить на сервер</Button></ListItem>)}
					{authCtx.authorised && (<ListItem><Button onClick={characterFromServer}>
						Загрузить из сервера</Button></ListItem>)}
					{authCtx.authorised && (<ListItem><Button onClick={onLogout}>
						Выйти</Button></ListItem>)}
					<Divider sx={{ m: 1 }} />
					<ListItem><Button disabled={true}>
						В режим ГМ'а</Button></ListItem>
					<ListItem><Button onClick={() => { navigate("/character-selection"); }}>
						Выбор персонажа</Button></ListItem>
					<ListItem><Button onClick={exportJson}>
						Экспорт в JSON</Button></ListItem>
					<Divider sx={{ m: 1 }} />
					<ListItem><Button onClick={toggleEditMode}>
						{editMode ? "Завершить редактирование" : "Редактировать"}</Button></ListItem>
					<ListItem><Button disabled={true}>
						Выбор конфигурации</Button></ListItem>
					<Divider sx={{ m: 1 }} />
					<Typography margin={2} variant="h5">Виджеты</Typography>
					<ListItem><Button onClick={() => addWidget("health-view")}>
						Счётчик здоровья</Button></ListItem>
					<ListItem><Button onClick={() => addWidget("level-view")}>
						Счётчик уровня</Button></ListItem>
					<ListItem><Button onClick={() => addWidget("armor-view")}>
						Счётчик класса защиты</Button></ListItem>
					<ListItem><Button onClick={() => addWidget("skills-view")}>
						Панель навыков</Button></ListItem>
					<ListItem><Button onClick={() => addWidget("attributes-view")}>
						Панель характеристик</Button></ListItem>
				</List>
			</Drawer>
		</Box>
	)
}

export default PlayerScreen;
