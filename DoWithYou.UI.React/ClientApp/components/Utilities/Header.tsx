import * as React from 'react';
import * as Misc from './Misc';
import { LoginButton, RegisterButton } from './Button';

export class Header extends React.Component<{}, {}> {
    render() {
        return (
            <div id='header'>
                <div>
                    <LoginButton />
                    <RegisterButton />
                </div>
                <Misc.ClearFix />
            </div>
        );
    }
}