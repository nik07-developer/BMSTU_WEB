import { useContext, useState } from "react";
import { ApiContext } from "../context/ApiProvider";
import { isAxiosError } from "axios";
import { Character, CharacterScreen } from "../model/Model";

export enum FetchState {
	DEFAULT = 'DEFAULT',
	LOADING = 'LOADING',
	SUCCESS = 'SUCCESS',
	ERROR = 'ERROR'
}

export type FetchError = {
	state: FetchState,
	message?: string
}

export function useUserLogin() {
	const [fetchError, setFetchError] = useState<FetchError>({ state: FetchState.DEFAULT });
	const ctx = useContext(ApiContext);

	const login = async (login: string, password: string) => {
		try {
			setFetchError({ state: FetchState.LOADING });
			const response = await ctx.axiosInstance.post('/login', { login, password });
			console.log(response);
			// TODO: check response
			const token = response.data.auth_token as string;
			localStorage.setItem("token", token);
			ctx.setAuthorised(true);
			setFetchError({ state: FetchState.SUCCESS });
		}
		catch (e: unknown) {
			if (isAxiosError(e)) {
				if (e.code == "ERR_BAD_REQUEST") {
					setFetchError({ state: FetchState.ERROR, message: "Неверное имя пользователя и/или пароль" });
				}
				else {
					setFetchError({ state: FetchState.ERROR, message: e.message });
				}
			}
			else if (e instanceof Error) {
				setFetchError({ state: FetchState.ERROR, message: e.message });
			}
			else {
				setFetchError({ state: FetchState.ERROR, message: "Неизвестная ошибка" });
			}
		}
	};

	return [fetchError, login] as const;
}

export function useUserLogout() {
	const [fetchError, setFetchError] = useState<FetchError>({ state: FetchState.DEFAULT });
	const ctx = useContext(ApiContext);

	const logout = async () => {
		try {
			setFetchError({ state: FetchState.LOADING });
			const response = await ctx.axiosInstance.post('/logout');
			setFetchError({ state: FetchState.SUCCESS });
		}
		catch (e: unknown) {
			if (isAxiosError(e) || e instanceof Error) {
				setFetchError({ state: FetchState.ERROR, message: e.message });
			}
			else {
				setFetchError({ state: FetchState.ERROR, message: "Неизвестная ошибка" });
			}
		}
		finally {
			localStorage.removeItem("token");
			ctx.setAuthorised(false);
		}
	};

	return [fetchError, logout] as const;
}

export function useUserRegister() {
	const [fetchError, setFetchError] = useState<FetchError>({ state: FetchState.DEFAULT });
	const ctx = useContext(ApiContext);

	const register = async (login: string, password: string, name: string) => {
		try {
			setFetchError({ state: FetchState.LOADING });
			const response = await ctx.axiosInstance.post('/users', {login, password, name});
			setFetchError({ state: FetchState.SUCCESS });
		}
		catch (e: unknown) {
			if (isAxiosError(e) || e instanceof Error) {
				setFetchError({ state: FetchState.ERROR, message: e.message });
			}
			else {
				setFetchError({ state: FetchState.ERROR, message: "Неизвестная ошибка" });
			}
		}
	};

	return [fetchError, register] as const;
}


export function useUserGetCharacterList() {
	const [fetchError, setFetchError] = useState<FetchError>({ state: FetchState.DEFAULT });
	const ctx = useContext(ApiContext);

	const getCharacterList = async (setCharacters: (characters: Character[]) => void) => {
		try {
			setFetchError({ state: FetchState.LOADING });
			const response = await ctx.axiosInstance.get('/characters');
			const characters = response.data as Character[];
			setCharacters(characters);
			setFetchError({ state: FetchState.SUCCESS });
		}
		catch (e: unknown) {
			if (isAxiosError(e)) {
				setFetchError({ state: FetchState.ERROR, message: e.message });
			}
			else if (e instanceof Error) {
				setFetchError({ state: FetchState.ERROR, message: e.message });
			}
			else {
				setFetchError({ state: FetchState.ERROR, message: "Неизвестная ошибка" });
			}
		}
	};

	return [fetchError, getCharacterList] as const;
}

export function useUserGetCharacter(id: string) {
	const [fetchError, setFetchError] = useState<FetchError>({ state: FetchState.DEFAULT });
	const ctx = useContext(ApiContext);

	const getCharacter = async (setCharacter: (characters: Character) => void) => {
		try {
			setFetchError({ state: FetchState.LOADING });
			const response = await ctx.axiosInstance.get(`/character/${id}`);
			const characters = response.data as Character;
			setFetchError({ state: FetchState.SUCCESS });
		}
		catch (e: unknown) {
			if (isAxiosError(e)) {
				setFetchError({ state: FetchState.ERROR, message: e.message });
			}
			else if (e instanceof Error) {
				setFetchError({ state: FetchState.ERROR, message: e.message });
			}
			else {
				setFetchError({ state: FetchState.ERROR, message: "Неизвестная ошибка" });
			}
		}
	};

	return [fetchError, getCharacter] as const;
}

export function useUserCreateCharacter(character: Character) {
	const [fetchError, setFetchError] = useState<FetchError>({ state: FetchState.DEFAULT });
	const ctx = useContext(ApiContext);

	const updateCharacter = async () => {
		try {
			setFetchError({ state: FetchState.LOADING });
			const response = await ctx.axiosInstance.post(`/characters`, character);
			setFetchError({ state: FetchState.SUCCESS });
		}
		catch (e: unknown) {
			if (isAxiosError(e)) {
				setFetchError({ state: FetchState.ERROR, message: e.message });
			}
			else if (e instanceof Error) {
				setFetchError({ state: FetchState.ERROR, message: e.message });
			}
			else {
				setFetchError({ state: FetchState.ERROR, message: "Неизвестная ошибка" });
			}
		}
	};

	return [fetchError, updateCharacter] as const;
}

export function useUserUpdateCharacter(id: string, character: Character) {
	const [fetchError, setFetchError] = useState<FetchError>({ state: FetchState.DEFAULT });
	const ctx = useContext(ApiContext);

	const updateCharacter = async () => {
		try {
			setFetchError({ state: FetchState.LOADING });
			const response = await ctx.axiosInstance.patch(`/character/${id}`, character);
			setFetchError({ state: FetchState.SUCCESS });
		}
		catch (e: unknown) {
			if (isAxiosError(e)) {
				setFetchError({ state: FetchState.ERROR, message: e.message });
			}
			else if (e instanceof Error) {
				setFetchError({ state: FetchState.ERROR, message: e.message });
			}
			else {
				setFetchError({ state: FetchState.ERROR, message: "Неизвестная ошибка" });
			}
		}
	};

	return [fetchError, updateCharacter] as const;
}

export function useUserDeleteCharacter(id: string) {
	const [fetchError, setFetchError] = useState<FetchError>({ state: FetchState.DEFAULT });
	const ctx = useContext(ApiContext);

	const updateCharacter = async () => {
		try {
			setFetchError({ state: FetchState.LOADING });
			const response = await ctx.axiosInstance.patch(`/character/${id}`);
			setFetchError({ state: FetchState.SUCCESS });
		}
		catch (e: unknown) {
			if (isAxiosError(e)) {
				setFetchError({ state: FetchState.ERROR, message: e.message });
			}
			else if (e instanceof Error) {
				setFetchError({ state: FetchState.ERROR, message: e.message });
			}
			else {
				setFetchError({ state: FetchState.ERROR, message: "Неизвестная ошибка" });
			}
		}
	};

	return [fetchError, updateCharacter] as const;
}

export function useUserGetScreens(id: string) {
	const [fetchError, setFetchError] = useState<FetchError>({ state: FetchState.DEFAULT });
	const ctx = useContext(ApiContext);

	const getScreens = async (setScreens: (screens: CharacterScreen[]) => void) => {
		try {
			setFetchError({ state: FetchState.LOADING });
			const response = await ctx.axiosInstance.get(`/character/${id}/view-configs`);
			setScreens([response.data as CharacterScreen]);
			setFetchError({ state: FetchState.SUCCESS });
		}
		catch (e: unknown) {
			if (isAxiosError(e)) {
				setFetchError({ state: FetchState.ERROR, message: e.message });
			}
			else if (e instanceof Error) {
				setFetchError({ state: FetchState.ERROR, message: e.message });
			}
			else {
				setFetchError({ state: FetchState.ERROR, message: "Неизвестная ошибка" });
			}
		}
	};

	return [fetchError, getScreens] as const;
}
