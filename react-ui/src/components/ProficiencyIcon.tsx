import { SvgIcon, useTheme } from "@mui/material";
import { SkillProficiency } from "../model/Model";

function IconUntrained() {
    const theme = useTheme();
    return (
        <svg
            xmlns="http://www.w3.org/2000/svg"
            width="10"
            height="10"
            version="1.1"
            viewBox="0 0 3 3"
        >
            <circle
                cx="1.5"
                cy="1.5"
                r="1.2"
                fill="none"
                stroke={theme.palette.primary.main}
                strokeOpacity="1"
                strokeWidth="0.25"
                display="none"
            ></circle>
            <circle
                cx="1.5"
                cy="1.5"
                r="0.65"
                fill="none"
                fillOpacity="1"
                stroke={theme.palette.primary.main}
                strokeOpacity="1"
                strokeWidth="0.25"
            ></circle>
        </svg>
    );
}

function IconTrained() {
    const theme = useTheme();
    return (
        <svg
            xmlns="http://www.w3.org/2000/svg"
            width="10"
            height="10"
            version="1.1"
            viewBox="0 0 3 3"
        >
            <circle
                cx="1.5"
                cy="1.5"
                r="1.2"
                fill="none"
                stroke={theme.palette.primary.main}
                strokeOpacity="1"
                strokeWidth="0.25"
                display="none"
            ></circle>
            <circle
                cx="1.5"
                cy="1.5"
                r="0.65"
                fill={theme.palette.primary.main}
                fillOpacity="1"
                stroke={theme.palette.primary.main}
                strokeOpacity="1"
                strokeWidth="0.25"
            ></circle>
        </svg>
    );
}

function IconExpert() {
    const theme = useTheme();
    return (
        <svg
            xmlns="http://www.w3.org/2000/svg"
            width="10"
            height="10"
            version="1.1"
            viewBox="0 0 3 3"
        >
            <circle
                cx="1.5"
                cy="1.5"
                r="1.2"
                fill="none"
                stroke={theme.palette.primary.main}
                strokeOpacity="1"
                strokeWidth="0.25"
                display="inline"
            ></circle>
            <circle
                cx="1.5"
                cy="1.5"
                r="0.65"
                fill={theme.palette.primary.main}
                fillOpacity="1"
                stroke={theme.palette.primary.main}
                strokeOpacity="1"
                strokeWidth="0.25"
            ></circle>
        </svg>
    );
}

function ButtonProficiency(proficiency: SkillProficiency) {
    const IconSelect = (p: SkillProficiency) => {
        switch (p) {
            case SkillProficiency.Untrained:
                return IconUntrained();
            case SkillProficiency.Trained:
                return IconTrained();
            case SkillProficiency.Expert:
                return IconExpert();
        }
    }

    return <SvgIcon>{IconSelect(proficiency)}</SvgIcon>;
}

export default ButtonProficiency;
