import { useContext, useState } from "react";
import { ApiContext } from "../context/ApiProvider";
import { isAxiosError } from "axios";

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
			//const token = "123";

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
