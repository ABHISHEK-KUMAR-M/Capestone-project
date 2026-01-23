import { CanActivateFn } from '@angular/router';

export const userAccessGuard: CanActivateFn = (route, state) => {

  let name=sessionStorage.getItem('empName');
  if(name) {
    return true;
  }
  return false;

};

