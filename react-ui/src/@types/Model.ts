import { ReactElement } from "react";

export interface Widget {
	getHtml: (characterData: CharacterData, posx: number, posy: number) => ReactElement;
	name: string;
}

export type ScreenWidget = {
	widget: Widget;
	posx: number;
	posy: number;
}

export type CharacterScreen = {
	widgets: ScreenWidget[];
}

export type CharacterAttribute = {
	name: string,
	name_short: string,
	value: number
}

export type CharacterData = {
	maxhealth: number;
	attributes: CharacterAttribute[];
}

export type Character = {
	name: string;
	data: CharacterData;
	screens: CharacterScreen[];
}
