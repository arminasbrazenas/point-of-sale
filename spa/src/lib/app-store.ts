import { create } from 'zustand';

export interface ApplicationUser {
  id: number;
  businessId: number | null;
  role: string;
}

interface AppState {
  applicationUser: ApplicationUser | null;
  setApplicationUser: (applicationUser: ApplicationUser | null) => void;
  updateApplicationUser: (updates: Partial<ApplicationUser>) => void; // Update specific fields
  resetApplicationUser: () => void; // Reset to null
}

export const useAppStore = create<AppState>((set) => ({
  applicationUser: null,

  // Replace the entire application user state
  setApplicationUser: (applicationUser) =>
    set((state) => ({
      applicationUser: applicationUser ? { ...applicationUser } : null, // Always create a new object
    })),

  // Update specific fields in application user state
  updateApplicationUser: (updates) =>
    set((state) => ({
      applicationUser: state.applicationUser
        ? { ...state.applicationUser, ...updates } // Merge updates into a new object
        : null,
    })),

  // Reset state to null
  resetApplicationUser: () =>
    set(() => ({
      applicationUser: null,
    })),
}));