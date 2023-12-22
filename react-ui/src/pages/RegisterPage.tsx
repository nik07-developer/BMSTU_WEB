import LineEdit from "../components/LineEdit";

function RegisterPage() {
    return (
        <div className="vertical-distribute">
            <div></div>
            <div className="center vertical-list">
                <form className="center vertical-list visible">
                    <h1>Регистрация</h1>
                    <LineEdit label="ИМЯ ПОЛЬЗОВАТЕЛЯ"/>
                    <LineEdit label="ЭЛЕКТРОННАЯ ПОЧТА"/>
                    <LineEdit label="ПАРОЛЬ" hidden={true}/>
                    <LineEdit label="ПОДТВЕРЖДЕНИЕ ПАРОЛЯ" hidden={true}/>
                    <button className="button accented" style={{margin: 15, fontSize: 24}}>Зарегистрироваться</button>
                </form>
            </div>
            <div></div>
        </div>
    )
}

export default RegisterPage;