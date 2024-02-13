import { useContext, useState } from "react";
import { Character, ScreenWidget, characterClone } from "../model/Model";
import { useUserLogout } from "../api/ApiHooks";
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

interface PlayerScreenProps {
	character: Character,
	setCharacter: (character: Character) => void;
}

function PlayerScreen({ character, setCharacter }: PlayerScreenProps) {
	const navigate = useNavigate();
	const theme = useTheme();
	const [fetchError, onLogout] = useUserLogout();
	const [leftOpen, setLeftOpen] = useState(true);
	const [rightOpen, setRightOpen] = useState(true);
	const [activeScreen, setActiveScreen] = useState(0);
	const [editMode, setEditMode] = useState(false);

	const exportJson = () => {
		const jsonString = `data:text/json;chatset=utf-8,${encodeURIComponent(
			JSON.stringify(character)
		)}`;
		const link = document.createElement("a");
		link.href = jsonString;
		link.download = character.name + ".json";

		link.click();
	};

	const characterRename = (ev: any) => {
		let chr = characterClone(character);
		chr.name = ev.target.value || "";
		setCharacter(chr);
	}

	return (
		<Box sx={{ display: 'flex' }}>
			<Box>
				<IconButton sx={{ m: 2 }} onClick={() => { setLeftOpen(true); }}><MenuIcon /></IconButton>
				{character && (character.screens[activeScreen].widgets.map((wx: ScreenWidget, wxIndex: number) => {
					return (
						<Draggable defaultPosition={{ x: wx.posx, y: wx.posy }} disabled={!editMode}
							onStop={(e: DraggableEvent, data: DraggableData) => {
								let chr = characterClone(character);
								chr.screens[activeScreen].widgets[wxIndex].posx = data.lastX;
								chr.screens[activeScreen].widgets[wxIndex].posy = data.lastY;
								setCharacter(chr);
							}}>
							<Box sx={{ position: "absolute", top: "0", left: "0" }}>
								{htmlByWxName(wx.name)(character, setCharacter, editMode)}
							</Box>
						</Draggable>)
				}))}
			</Box>
			<Drawer variant="persistent" anchor="left" open={leftOpen}>
				<Box display="flex" flexDirection="row" alignItems="center" justifyContent="space-between" sx={{ backgroundColor: theme.palette.secondary.main }}>
					{editMode && (
						<TextField sx={{ m: 2, minWidth: "200px" }} size="medium" variant="standard"
							defaultValue={character.name} onChange={characterRename} />
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
					<ListItem><Button onClick={() => { navigate("/login"); }}>
						Войти</Button></ListItem>
					<ListItem><Button onClick={() => { navigate("/register"); }}>
						Зарегистрироваться</Button></ListItem>
					<Divider sx={{ m: 1 }} />
					<ListItem><Button disabled={true}>
						В режим ГМ'а</Button></ListItem>
					<ListItem><Button onClick={() => { navigate("/character-selection"); }}>
						Выбор персонажа</Button></ListItem>
					<ListItem><Button onClick={exportJson}>
						Экспорт в JSON</Button></ListItem>
					<Divider sx={{ m: 1 }} />
					<ListItem><Button onClick={() => { setEditMode(!editMode) }}>
						{editMode ? "Завершить редактирование" : "Редактировать"}</Button></ListItem>
					<ListItem><Button disabled={true}>
						Выбор конфигурации</Button></ListItem>
				</List>
			</Drawer>
		</Box>
	)
}

export default PlayerScreen;
