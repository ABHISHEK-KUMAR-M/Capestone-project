import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { userAccessGuard } from './user-access-guard';

describe('userAccessGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => userAccessGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
