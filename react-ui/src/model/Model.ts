import { ReactElement } from "react";

export type CharacterAttribute = {
	value: number;
}

export enum SkillProficiency {
	Untrained = "U",
	Trained = "T",
	Expert = "E"
}

export type Skill = {
	proficiency: SkillProficiency;
}

export type Skills = {
	acrobatics: Skill;
	athletics: Skill;
	perception: Skill;
	survival: Skill;
	animal_handling: Skill;
	intimidation: Skill;
	perfomance: Skill;
	history: Skill;
	sleight_of_hand: Skill;
	magic: Skill;
	medicine: Skill;
	deception: Skill;
	nature: Skill;
	insight: Skill;
	investigation: Skill;
	religion: Skill;
	stealth: Skill;
	persuasion: Skill;
}

export type CharacterAttributes = {
	strength: CharacterAttribute;
	dexterity: CharacterAttribute;
	constitution: CharacterAttribute;
	intelligence: CharacterAttribute;
	wisdom: CharacterAttribute;
	charisma: CharacterAttribute;
}

export type Character = {
	id?: string;
	name: string;
	max_health: number;
	health: number;
	level: number;
	armor_class: number;
	attributes: CharacterAttributes;
	skills: Skills;
	screens: CharacterScreen[];
}

export function characterClone(c: Character) {
	let cloned_screens: CharacterScreen[] = [];
	c.screens.forEach(v => cloned_screens.push({ name: v.name, widgets: [...v.widgets] }));

	const result: Character = {
		...c,
		attributes: {...c.attributes},
		skills: {...c.skills},
		screens: cloned_screens
	};

	return result;
}

export type ScreenWidget = {
	name: string;
	posx: number;
	posy: number;
}

export type CharacterScreen = {
	name: string,
	widgets: ScreenWidget[];
}

export function attributeDisplayName(attribute: string) {
	const attrs = new Map<string, [string, string]>([
		["strength", ["Сила", "сил"]],
		["dexterity", ["Ловкость", "лов"]],
		["constitution", ["Выносливость", "тел"]],
		["intelligence", ["Интеллект", "инт"]],
		["wisdom", ["Мудрость", "муд"]],
		["charisma", ["Харизма", "хар"]]
	]);

	return attrs.get(attribute) || ["Неизвестный атрибут", "???"];
}

export function attributeModifier(attribute: number) {
	return Math.floor((attribute - 10) / 2);
}

export function skillInfo(skill: string) {
	const skills = new Map<string, [string, string]>([
		["acrobatics", ["Акробатика", "dexterity"]],
		["athletics", ["Атлетика", "strength"]],
		["perception", ["Восприятие", "wisdom"]],
		["survival", ["Выживание", "wisdom"]],
		["animal_handling", ["Уход за животными", "wisdom"]],
		["intimidation", ["Запугивание", "charisma"]],
		["perfomance", ["Выступление", "charisma"]],
		["history", ["История", "intelligence"]],
		["sleight_of_hand", ["Ловкость рук", "dexterity"]],
		["magic", ["Магия", "intelligence"]],
		["medicine", ["Медицина", "wisdom"]],
		["deception", ["Обман", "charisma"]],
		["nature", ["Природа", "wisdom"]],
		["insight", ["Проницательность", "wisdom"]],
		["investigation", ["Расследование", "intelligence"]],
		["religion", ["Религия", "intelligence"]],
		["stealth", ["Скрытность", "dexterity"]],
		["persuasion", ["Убеждение", "charisma"]],
	]);

	return skills.get(skill) || ["Неизвестный навык", "unknown"];
}
