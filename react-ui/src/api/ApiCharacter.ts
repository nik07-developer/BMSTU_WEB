import { useContext, useState } from "react";
import { FetchError, FetchState } from "./ApiHooks";
import { ApiContext } from "../context/ApiProvider";
import { Character, CharacterAttributes, Skills } from "../model/Model";
import { isAxiosError } from "axios";
import { Guid } from "guid-typescript";

export type ApiCharacter = {
	character_id: string;
	name: string;
	max_health: number;
	health: number;
	level: number;
	armor_class: number;
	attributes: CharacterAttributes;
	skills: Skills;
}

export function useApiGetCharacters() {
	const [fetchError, setFetchError] = useState<FetchError>({ state: FetchState.DEFAULT });
	const ctx = useContext(ApiContext);

	const getCharacterList = async (setCharacters: (characters: ApiCharacter[]) => void) => {
		try {
			setFetchError({ state: FetchState.LOADING });
			const response = await ctx.axiosInstance.get('/characters');
			const characters = response.data as ApiCharacter[];

			setCharacters(characters);
			setFetchError({ state: FetchState.SUCCESS });
		}
		catch (e: unknown) {
            const msg = (isAxiosError(e) && e.message)
                || (e instanceof Error && e.message)
                || "Неизвестная ошибка";
            setFetchError({ state: FetchState.ERROR, message: msg });
		}
	};

	return [fetchError, getCharacterList] as const;
}

export function useApiGetCharacter() {
	const [fetchError, setFetchError] = useState<FetchError>({ state: FetchState.DEFAULT });
	const ctx = useContext(ApiContext);

	const getCharacter = async (guid: Guid, setCharacter: (character: ApiCharacter) => void) => {
		try {
			setFetchError({ state: FetchState.LOADING });
			const response = await ctx.axiosInstance.get(`/character/${guid.toString()}`);
            setCharacter(response.data as ApiCharacter);
			setFetchError({ state: FetchState.SUCCESS });
		}
		catch (e: unknown) {
            const msg = (isAxiosError(e) && e.message)
                || (e instanceof Error && e.message)
                || "Неизвестная ошибка";
            setFetchError({ state: FetchState.ERROR, message: msg });
		}
	};

	return [fetchError, getCharacter] as const;
}

export function useApiCreateCharacter() {
	const [fetchError, setFetchError] = useState<FetchError>({ state: FetchState.DEFAULT });
	const ctx = useContext(ApiContext);

	const createCharacter = async (character: ApiCharacter, setGuid: (guid: Guid) => void) => {
		try {
			setFetchError({ state: FetchState.LOADING });
			const response = await ctx.axiosInstance.post(`/characters`, character);
            setGuid(response.data.character_id);
			setFetchError({ state: FetchState.SUCCESS });
		}
		catch (e: unknown) {
            const msg = (isAxiosError(e) && e.message)
                || (e instanceof Error && e.message)
                || "Неизвестная ошибка";
            setFetchError({ state: FetchState.ERROR, message: msg });
		}
	};

	return [fetchError, createCharacter] as const;
}

export function useUpdateCharacter() {
	const [fetchError, setFetchError] = useState<FetchError>({ state: FetchState.DEFAULT });
	const ctx = useContext(ApiContext);

	const updateCharacter = async (guid: Guid, character: ApiCharacter) => {
		try {
			setFetchError({ state: FetchState.LOADING });
			const response = await ctx.axiosInstance.patch(`/character/${guid.toString()}`, character);
			setFetchError({ state: FetchState.SUCCESS });
		}
		catch (e: unknown) {
            const msg = (isAxiosError(e) && e.message)
                || (e instanceof Error && e.message)
                || "Неизвестная ошибка";
            setFetchError({ state: FetchState.ERROR, message: msg });
		}
	};

	return [fetchError, updateCharacter] as const;
}

export function useDeleteCharacter() {
	const [fetchError, setFetchError] = useState<FetchError>({ state: FetchState.DEFAULT });
	const ctx = useContext(ApiContext);

	const updateCharacter = async (guid: Guid) => {
		try {
			setFetchError({ state: FetchState.LOADING });
			const response = await ctx.axiosInstance.delete(`/character/${guid.toString()}`);
			setFetchError({ state: FetchState.SUCCESS });
		}
		catch (e: unknown) {
            const msg = (isAxiosError(e) && e.message)
                || (e instanceof Error && e.message)
                || "Неизвестная ошибка";
            setFetchError({ state: FetchState.ERROR, message: msg });
		}
	};

	return [fetchError, updateCharacter] as const;
}
