import { useContext, useState } from "react";
import { ApiContext } from "../context/ApiProvider";
import { FetchError, FetchState } from "./ApiHooks";
import { Guid } from "guid-typescript";
import { CharacterScreen } from "../model/Model";
import { isAxiosError } from "axios";

export function useApiGetCharacterScreens() {
	const [fetchError, setFetchError] = useState<FetchError>({ state: FetchState.DEFAULT });
	const ctx = useContext(ApiContext);

	const getScreens = async (guid: Guid, setScreens: (screens: CharacterScreen[]) => void) => {
		try {
			setFetchError({ state: FetchState.LOADING });
			const response = await ctx.axiosInstance.get(`/character/${guid.toString()}/view-configs`);
			setScreens([response.data as CharacterScreen]);
			setFetchError({ state: FetchState.SUCCESS });
		}
		catch (e: unknown) {
            const msg = (isAxiosError(e) && e.message)
                || (e instanceof Error && e.message)
                || "Неизвестная ошибка";
            setFetchError({ state: FetchState.ERROR, message: msg });
		}
	};

	return [fetchError, getScreens] as const;
}
