import { ReactElement } from "react";

export interface Widget {
	getHtml: (data: CharacterData, setData: (data: CharacterData) => void) => ReactElement;
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
	value: number
}

export enum SkillProficiency {
	Untrained = "U",
	Trained = "T",
	Expert = "E"
}

export type Skill = {
	name: string;
	proficiency: SkillProficiency;
}

export type CharacterData = {
	max_health: number;
	health: number;
	level: number;
	armor_class: number;
	attributes: CharacterAttribute[];
	skills: Skill[];
}

export type Character = {
	name: string;
	data: CharacterData;
	screens: CharacterScreen[];
}

export function attributeDisplayName(attribute: string) {
	const attrs = new Map<string, [string, string]>([
		["strength", [ "Сила", "сил" ]],
		["dexterity", [ "Ловкость", "лов" ]],
		["constitution", [ "Выносливость", "тел" ]],
		["intelligence", [ "Интеллект", "инт" ]],
		["wisdom", [ "Мудрость", "муд" ]],
		["charisma", [ "Харизма", "хар"]]
	]);

	return attrs.get(attribute) || ["Неизвестный атрибут", "???"];
}

export function attributeModifier(attribute: number) {
	return Math.floor((attribute - 10) / 2);
}

export function skillInfo(skill: string) {
	const skills = new Map<string, [string, string]>([
		[ "acrobatics", [ "Акробатика", "dexterity" ]],
		[ "athletics", [ "Атлетика", "strength" ]],
		[ "perception", [ "Восприятие", "wisdom" ]],
		[ "survival", [ "Выживание", "wisdom" ]],
		[ "animal_handling", [ "Уход за животными", "wisdom" ]],
		[ "intimidation", [ "Запугивание", "charisma" ]],
		[ "perfomance", [ "Выступление", "charisma" ]],
		[ "history", [ "История", "intelligence" ]],
		[ "sleight_of_hand", [ "Ловкость рук", "dexterity" ]],
		[ "magic", [ "Магия", "intelligence" ]],
		[ "medicine", [ "Медицина", "wisdom" ]],
		[ "deception", [ "Обман", "charisma" ]],
		[ "nature", [ "Природа", "wisdom" ]],
		[ "insight", [ "Проницательность", "wisdom" ]],
		[ "investigation", [ "Расследование", "intelligence" ]],
		[ "religion", [ "Религия", "intelligence" ]],
		[ "stealth", [ "Скрытность", "dexterity" ]],
		[ "persuasion", [ "Убеждение", "charisma" ]],
	]);

	return skills.get(skill) || ["Неизвестный навык", "unknown"];
}

/*
{
	id: 123,
	name: "Leeroy Jenkins",
	level: 10,
	armor_class: 13,
	max_health: 100,
	health: 50,
	attributes: [
		{
			name: "strength",
			value: 10,
			saving_throw: "U"  // ("T" or "E")
		}
		...
	]
	skills: [
		{
			name: "athletics",
			proficiency: "U"  // ("T" or "E")
		}
	]
}
*/
