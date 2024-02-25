import { useContext, useState } from "react";
import { ApiContext } from "../context/ApiProvider";
import { isAxiosError } from "axios";
import { Character, CharacterScreen } from "../model/Model";
import { Guid } from "guid-typescript";

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
            const msg = (isAxiosError(e) && e.message)
                || (e instanceof Error && e.message)
                || "Неизвестная ошибка";
            setFetchError({ state: FetchState.ERROR, message: msg });
		}
	};

	return [fetchError, register] as const;
}
